import os
import requests
import sqlite3
from urllib.parse import urlparse

# Step 1: Connect to the SQLite database
db_path = 'items.db'  # Replace with your database path
conn = sqlite3.connect(db_path)
cursor = conn.cursor()

# Step 2: Create a directory to store the images
image_directory = 'Resources/Icons'  # Define your directory
if not os.path.exists(image_directory):
    os.makedirs(image_directory)

# Step 3: Fetch the URLs from the database
cursor.execute("SELECT Icon FROM items")  # Replace with your table name
rows = cursor.fetchall()

# Step 4: Traverse through each row, fetch the image, and store it
for row in rows:
    icon_url = row[0]  # Assuming the second column is the URL
    
    # Fetch the image
    try:
        parsed_url = urlparse(f"https://cafemaker.wakingsands.com/{icon_url}")
        image_filename = os.path.basename(parsed_url.path)  # Extract the filename from the URL
        
        # Fetch the image
        response = requests.get(f"https://cafemaker.wakingsands.com/{icon_url}")
        response.raise_for_status()  # Check if the request was successful
        
        # Save the image with its original name
        image_path = os.path.join(image_directory, image_filename)
        with open(image_path, 'wb') as f:
            f.write(response.content)
        print(f"Downloaded and saved image {image_filename}")
        
    except requests.exceptions.RequestException as e:
        print(f"Failed to download image from {icon_url}: {e}")

# Step 5: Close the database connection
conn.close()
