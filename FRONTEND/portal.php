<?php

session_start();

echo "<pre>";
echo "SESSION ID: " . session_id() . PHP_EOL;
echo "USER ID: ";
var_dump($_SESSION["user_id"] ?? null);
echo "EMAIL: ";
var_dump($_SESSION["email"] ?? null);
echo "</pre>";

exit; ?>

<!DOCTYPE html>
<html lang="es">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Mi perfil</title>
</head>

<body>

    <h1>
        Bienvenido, <?= htmlspecialchars($user["Nombre"]) ?>
    </h1>

    <p>
        Usuario:
        <?= htmlspecialchars($user["Usuario"]) ?>
    </p>

    <p>
        Email:
        <?= htmlspecialchars($user["Email"]) ?>
    </p>

    <p>
        ID:
        <?= htmlspecialchars($user["Id"]) ?>
    </p>

    <p>
        Estado: Activo
    </p>

    <a href="/logout">Cerrar sesión</a>

</body>

</html>