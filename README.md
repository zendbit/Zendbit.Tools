# Zendbit.Tools
Small helper for code productivity

License MIT

Usage Install via nuget Zendbit.Tools

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
