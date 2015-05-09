<?php

//
// Copyright (c) 2011 Scott Clayton
//
// This file is part of the C# to PHP Encryption Library.
//   
// The C# to PHP Encryption Library is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//   
// The C# to PHP Encryption Library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with the C# to PHP Encryption Library.  If not, see <http://www.gnu.org/licenses/>.
//

include('Crypt/RSA.php');

$code = str_replace(array('_','-'), array('/','+'), $_POST['code']);
$code = base64_decode($code);

$rsa = new Crypt_RSA();
$rsa->setEncryptionMode(CRYPT_RSA_ENCRYPTION_PKCS1);

// The key is hardcoded in on both the client and server side of this example
$rsa->loadKey("-----BEGIN RSA PRIVATE KEY-----
MIICXgIBAAKBgQCixMLZ4Ug97FDIncnKMAIbqcufTzIfBWF+nmysuDWKhIhTjc67
o5BrQGYJlGggEARJhnHwVA1XeHsO9l44pKtFoQVI7vsJqyHIJ0HCDAFzJGdoAVdd
kpLws0ayX+c6jtKwtMf7QZYv+Oihq88mC5eyt0BcdzEDzb1TvsP29HKGrQIDAQAB
AoGBAJRE7kYxLHCT+wa0jWOrldADPSRSrRKLAUOpJs+zQbp7ff+2trJAjcbVM93Y
HX3PeUhMuy+0MS8T5e08SZoJjqV7y4P35+NlkDg0OFoP/1fgK+0T58+hSQeA0plL
5gEWuRaQjnD0H10L/BWa17yPK0Us3vxMPkGsy1hzhia8v6fBAkEA0JjRPY9XOr6d
F9xgfhMEuL5vMkcFy3a95zj0AyMNqz1z9O2SO8YDXF/N9MPaH2aoAadIzF1IhJEU
ullJcXnKsQJBAMfB3h5iSZdSRy82QLWyvYxQvL2iA6orsmJH8TfRbQYwE/Ls4944
FJVTUz3R9Ay2vutjYA9qs5noAvhu1hC6gr0CQBN9I5d3y/OOGYlAKre8uSU1jZgJ
8K2ow2dV995PKRjFng7VH2N8RZYc0VY78iYa5jl5UqDdWkggcepTKzxx35ECQQC2
C5ovuocynss83YaPkHtp+tJnR9VrKjOBmerdYcCoGPy1MOphxF4N0EhWWJa/V3Qa
9Q/APQ+8vVKnseroh/FJAkEAgUYfyaKCsrPA2GP8y5wohgz9828uy4seFDJKD1SA
h+oCNW4VbM0cHtaAqddQXxJE1Yf+2FFEh5+nmTZkH8neYA==
-----END RSA PRIVATE KEY-----
");
$plaintext = $rsa->decrypt($code);

echo "The text was " . $plaintext . ".";

?>