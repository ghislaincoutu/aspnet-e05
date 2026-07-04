#!/bin/bash

function apply_pause() {
  read -p "$*"
}

echo "Publication des fichiers Angular sur le serveur Web local"
apply_pause "Appuyer sur la touche [Retour] pour continuer..."

# Source (so)
so=/home/dev2607/Documents/XD01/aspnet-e05/angular05/dist/angular05/browser

# Destination (de)
de=/var/www/html/d003/aspnet-e05

rm $de/favicon.ico
rm $de/index.html
rm $de/main*.js
rm $de/polyfills*.js
rm $de/styles*.css
cp $so/*.* $de/
sed -i 's/<base href="\/">/<base href="\/d003\/aspnet-e05\/">/g' $de/index.html
