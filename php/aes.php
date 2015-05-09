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

include('Crypt/AES.php');

$aes = new Crypt_AES();

$aes->setKey('abcdefghijklmnopabcdefghijklmnop');
$aes->setIV('abcdefghijklmnop');
$aes->setKeyLength(256);

echo base64_encode("abcdefghijklmnopabcdefghijklmnop") . "<BR>";
echo base64_encode("abcdefghijklmnop") . "<BR>";

$plaintext = 'llamas are super cool!';

echo base64_encode($aes->encrypt($plaintext)) . "<BR>";

echo $aes->decrypt($aes->encrypt($plaintext));

?>