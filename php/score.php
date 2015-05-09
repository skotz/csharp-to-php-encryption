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

// Set the location to the public and private keys
$PrivateKeyFile = "private.php";
$PublicKeyFile  = "public.crt";
include("secure.php");

if ($AESMessage != "")
{
   // Get the username and high score from the message that was sent
   $split = explode(",", $AESMessage);
   $username = $split[0];
   $score = $split[1];

   $rank = "";
   if ($score < 100)
      $rank = "Loser!";
   else if ($score < 1000)
      $rank = "Not bad...";
   else if ($score < 10000)
      $rank = "Pretty Good.";
   else if ($score < 100000)
      $rank = "Amazing!";
   else if ($score < 1000000)
      $rank = "~YOU DA BOMB~";
   else
      $rank = "YOU ARE A GRAND MASTER!";
      
   SendEncryptedResponse("Name: " . $username . " Rank: " . $rank);
}

?>