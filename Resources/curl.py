import requests
import sqlite3
import json

# Step 1: Set up the SQLite database
def create_database():
    conn = sqlite3.connect('items.db')
    cursor = conn.cursor()
    cursor.execute('''CREATE TABLE IF NOT EXISTS items (
                    ID INTEGER PRIMARY KEY,
                    Name TEXT,
                    ClassJobCategoryName TEXT,
                    ItemUICategoryID INTEGER,
                    Icon TEXT)''')
    conn.commit()
    return conn

# Step 2: Fetch data from the API
def fetch_data(conn):
    page = 1
    while True:
        print(page)
        url = f"https://cafemaker.wakingsands.com/item?columns=ID,Name,ClassJobCategory.Name,ItemUICategory,Icon&limit=3000&page={page}"
        # url = f'https://cafemaker.wakingsands.com/item?columns=ID,Name,ClassJobCategory.Name&limit=3000&page={page}'
        response = requests.get(url)
        
        if response.status_code != 200:
            print(f"Error fetching page {page}: {response.status_code}")
            break
        
        data = response.json()
        
        # Step 3: Insert data into the SQLite database
        items = data.get('Results', [])

        if not items:
            print("No more items to fetch.")
            break
        
        with conn:
            cursor = conn.cursor()
            for item in items:
                if item['ItemUICategory'] != None:
                    cursor.execute('''INSERT OR IGNORE INTO items (ID, Name, ClassJobCategoryName, ItemUICategoryID, Icon)
                        VALUES (?, ?, ?, ?, ?)''',
                    (item['ID'], 
                        item['Name'], 
                        item['ClassJobCategory']['Name'], 
                        item['ItemUICategory']['ID'],
                        item['Icon']))
        
        print(f"Fetched and inserted page {page} with {len(items)} items.")
        page += 1

def delete_null_class_job_categories(conn):
    with conn:
        cursor = conn.cursor()
        cursor.execute('''
            DELETE FROM items
            WHERE ClassJobCategoryName IS NULL
        ''')
    print("Deleted rows where ClassJobCategory.Name is null.")

if __name__ == '__main__':
    conn = create_database()
    fetch_data(conn)
    delete_null_class_job_categories(conn)
    conn.close()