# Changelog

## [0.0.0] - 2024-10-10 Horace
### CreateCardView
- now you can search by category. itemUIcategoryID for weapon is 1-32 , 84-?. 
- now you can view icons. Default is 礼物盒 026107.png. 
- modified .csproj so "/resources/Icons" will be copied(updated) to Environment.CurrentDirectory

### DecryptCardView
- Created. As name suggests, show info of a decrypted card (either imported or from a closet
- Clicking on an item in the closet will direct to this.
- 尽量贴合了夏哥哥的 UI设计

### other
- Fixed bugs
- LockerViewModel uses direct reference to MainViewModel. Not the best approach, but it works. Made all viewmodels public to achieve this
- Consider putting all global constants in one file
- Need DyeIcons
- items.db is not auto copied into bin/debug/.../Resources? I manually moved it.
