#!/bin/bash

echo "===== $(date) ====="

cd /var/www/ProyectoX || {
    echo "ERROR: no se puede entrar en /var/www/ProyectoX"
    exit 1
}

echo "Directorio: $(pwd)"
echo "Usuario: $(whoami)"
echo "Git: $(/usr/bin/git --version)"

/usr/bin/git pull origin main