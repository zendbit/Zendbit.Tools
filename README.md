# Zendbit.Tools
Small helper for code productivity

License MIT

Search and install the nuget : Zendbit.Tools
```
Install-Package Zendbit.Tools -Version 1.0.6
```

```
using Zendbit.Tools.Encryption


// compute has with sha1
var hashed = SHA1Crypt.New().ComputeHash("this will hash");

// base64 encode
var encoded = Base64Crypt.New().Encode("string to be encode");
var decoded = Base64Crypt.New().Decode(encoded);

// XOR encryption
var xorEncrypted = XORCrypt.New().Encode("will encrpted");
var xorDecrypted = XORCrypt.New().Decode(xorEncrypted);
```

```
using Zendbit.Tools.IO

// read as byte async
var data = await FileOp.New().ReadBytesAsync("data.txt");

// read as text async
var data = await FileOp.New().ReadTextAsync("data.txt");

// read as byte
var data = FileOp.New().ReadBytes("data.txt");

// read as text
var data = FileOp.New().ReadText("data.txt");

// path helper
// path will return to specific os example for windows and unix is different
// parameter will using :: delimiter for the path
// example we use parameter
// mydata::files::sendit
// will convert to mydata/files/sendit on unix
// will convert to mydata\\files\\sendit on windows
var myPath = FileOp.New().Create("mydata::files::sendit")
```
