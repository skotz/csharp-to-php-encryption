@echo off
echo Place this BAT file in the bin folder of OpenSSL and run it to generate 
echo an RSA key pair. Just give the information it needs when it asks for it.
pause
openssl genrsa -aes256 -out temp.key 2048
openssl rsa -in temp.key -out private.key 
openssl req -new -x509 -nodes -sha1 -key private.key -out public.crt -days 3650
del temp.key
cls
echo Your keys have been generated.
echo private.key contains the private key
echo public.crt contains the public key
pause