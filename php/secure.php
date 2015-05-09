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

session_start();

include('Crypt/RSA.php');
include('Crypt/AES.php');

//$PrivateKeyFile = "private.key";
//$PublicKeyFile  = "public.crt";
$AESMessage     = "";

function Base64UrlDecode($x)
{
   return base64_decode(str_replace(array('_','-'), array('/','+'), $x));
}

function Base64UrlEncode($x)
{
   return str_replace(array('/','+'), array('_','-'), base64_encode($x));
}

//
// Use this function to save your private key in a php file so that people cannot download it.
//
function GeneratePHPKeyFile($private, $file)
{
   $rsa = file_get_contents($private);
   $rsa = "<?php \$PrivateRSAKey = \"" . $rsa . "\"; ?>";
   file_put_contents($file, $rsa);
   echo "The private key file has been stored in a php file. After you verify it works, ";
   echo "make sure you DELETE the private key (.key file) and DO NOT UPLOAD the .key file to your server.";
   exit;
}

//
// The remote user is requesting a public certificate.
//
if (isset($_POST['getkey']))
{
   echo file_get_contents($PublicKeyFile);
   exit;
}

//
// The remote user is sending an encrypted AES key and iv to use for encrypting.
//
if (isset($_POST['key']) && isset($_POST['iv']))
{
   include($PrivateKeyFile);

   $rsa = new Crypt_RSA();
   $rsa->setEncryptionMode(CRYPT_RSA_ENCRYPTION_PKCS1);
   $rsa->loadKey($PrivateRSAKey);

   $_SESSION['key'] = Base64UrlEncode($rsa->decrypt(Base64UrlDecode($_POST['key'])));
   $_SESSION['iv']  = Base64UrlEncode($rsa->decrypt(Base64UrlDecode($_POST['iv'])));

   SendEncryptedResponse("AES OK");
}

//
// The remote user is sending an AES encrypted message.
//
if ((isset($_SESSION['key']) && isset($_SESSION['iv'])) && isset($_POST['data']))
{
   $aes = new Crypt_AES(CRYPT_AES_MODE_CBC);

   $aes->setKeyLength(256);
   $aes->setKey(Base64UrlDecode($_SESSION['key']));
   $aes->setIV(Base64UrlDecode($_SESSION['iv']));
   $aes->enablePadding(); // This is PKCS

   $AESMessage = $aes->decrypt(Base64UrlDecode($_POST['data']));

   //
   // The remote client is requesting that we end the connection and destroy the session variables (the keys).
   //
   if ($AESMessage == "CLOSE CONNECTION")
   {
      echo Base64UrlEncode($aes->encrypt("DISCONNECTED"));

      $_SESSION['key'] = "llama";
      $_SESSION['iv']  = "llama";
      unset($_SESSION['key']);
      unset($_SESSION['iv']);
      session_destroy();
      exit;
   }
}

//
// Make sure you have not output any other text before or after calling this.
//
function SendEncryptedResponse($message)
{
   $aes = new Crypt_AES(CRYPT_AES_MODE_CBC);

   $aes->setKeyLength(256);
   $aes->setKey(Base64UrlDecode($_SESSION['key']));
   $aes->setIV(Base64UrlDecode($_SESSION['iv']));
   $aes->enablePadding(); // This is PKCS

   echo Base64UrlEncode($aes->encrypt($message));
   exit;
}
?>