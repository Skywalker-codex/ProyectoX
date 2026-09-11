<?php

session_start();

require_once __DIR__ . "/../db.php";

// Comprobar autenticación
if (!isset($_SESSION["user_id"])) {
    header("Location: /PORTAL/login.html");
    exit;
}

$userId = $_SESSION["user_id"];

// Obtener los datos del usuario
$sql = "
    SELECT 
        Id,
        Email,
        Nombre,
        Usuario,
        IsActive
    FROM usuarios
    WHERE Id = :id
      AND IsActive = 1
    LIMIT 1
";

$stmt = $pdo->prepare($sql);
$stmt->execute([
    ":id" => $userId
]);

$user = $stmt->fetch(PDO::FETCH_ASSOC);

// Si el usuario ya no existe o está desactivado
if (!$user) {
    session_unset();
    session_destroy();

    header("Location: /PORTAL/login.html");
    exit;
}

?>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

    <title>Mi perfil</title>

    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f4f6f8;
            margin: 0;
        }

        header {
            background: #222;
            color: white;
            padding: 20px;
            display: flex;
            justify-content: space-between;
            align-items: center;
        }

        .container {
            max-width: 800px;
            margin: 40px auto;
            padding: 20px;
        }

        .profile {
            background: white;
            border-radius: 12px;
            padding: 30px;
            box-shadow: 0 4px 15px rgba(0,0,0,0.08);
        }

        .profile h1 {
            margin-top: 0;
        }

        .data {
            margin-top: 20px;
        }

        .row {
            padding: 15px 0;
            border-bottom: 1px solid #eee;
        }

        .label {
            font-weight: bold;
            display: block;
            color: #666;
            margin-bottom: 5px;
        }

        .logout {
            color: white;
            text-decoration: none;
            background: #d9534f;
            padding: 10px 15px;
            border-radius: 6px;
        }
    </style>
</head>

<body>

<header>

    <div>
        Mi Portal
    </div>

    <a class="logout" href="/PORTAL/logout.php">
        Cerrar sesión
    </a>

</header>

<div class="container">

    <div class="profile">

        <h1>
            Hola, <?= htmlspecialchars($user["Nombre"]) ?>
        </h1>

        <p>
            Bienvenido a tu perfil.
        </p>

        <div class="data">

            <div class="row">
                <span class="label">Nombre</span>
                <?= htmlspecialchars($user["Nombre"]) ?>
            </div>

            <div class="row">
                <span class="label">Nombre de usuario</span>
                <?= htmlspecialchars($user["Usuario"]) ?>
            </div>

            <div class="row">
                <span class="label">Email</span>
                <?= htmlspecialchars($user["Email"]) ?>
            </div>

            <div class="row">
                <span class="label">ID de usuario</span>
                <?= htmlspecialchars($user["Id"]) ?>
            </div>

            <div class="row">
                <span class="label">Estado</span>
                Activo
            </div>

        </div>

    </div>

</div>

</body>
</html>