#!/bin/bash

function apply_pause() {
  read -p "$*"
}

echo "Publication des fichiers ASP.net et Angular sur le serveur privé virtuel OVH"
apply_pause "Appuyer sur la touche [Retour] pour continuer..."

# Source (so)
so=/var/www/html/d003/aspnet-e05

# Destination (de)
de=/var/www/vhost01/public_html/d003

echo -e "\e]11;#330033\a"
scp -r -P 2792 $so ubuntu@144.217.88.37:$de
echo -e "\e]11;#000000\a"
