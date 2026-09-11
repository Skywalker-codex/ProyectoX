#!/bin/bash

# Evitar ejecuciones simultáneas
exec 9>/var/run/proyectox-deploy.lock
flock -n 9 || exit 0

# Directorio del proyecto
cd /var/www/ProyectoX || exit 1

# Actualizar desde GitHub
/usr/bin/git pull origin main