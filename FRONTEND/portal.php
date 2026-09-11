```php
<?php

session_start();

if (!isset($_SESSION["user_id"])) {
    header("Location: /login");
    exit;
}

require_once "/var/www/ProyectoX/BACKEND/db.php";

$userId = $_SESSION["user_id"];

$sql = "
    SELECT TOP 1 Id, Email, Nombre, Usuario, IsActive
    FROM usuarios
    WHERE Id = :id
      AND IsActive = 1
";

$stmt = $pdo->prepare($sql);

$stmt->execute([
    ":id" => $userId
]);

$user = $stmt->fetch(PDO::FETCH_ASSOC);

if (!$user) {
    session_unset();
    session_destroy();

    header("Location: /login");
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

        * {
            box-sizing: border-box;
        }

        body {
            margin: 0;
            min-height: 100vh;

            display: flex;
            justify-content: center;
            align-items: center;

            font-family: Arial, sans-serif;

            background:
                linear-gradient(
                    rgba(15, 23, 42, 0.72),
                    rgba(30, 41, 59, 0.72)
                ),
                url("https://images.unsplash.com/photo-1519608487953-e999c86e7455?auto=format&fit=crop&w=2000&q=80");

            background-size: cover;
            background-position: center;
            background-attachment: fixed;

            padding: 20px;
        }

        .container {
            width: 100%;
            max-width: 460px;

            padding: 36px;

            background: rgba(255, 255, 255, 0.95);

            border-radius: 18px;

            box-shadow:
                0 20px 50px rgba(0, 0, 0, 0.25);

            text-align: center;
        }

        .avatar {
            width: 90px;
            height: 90px;

            margin: 0 auto 20px;

            display: flex;
            justify-content: center;
            align-items: center;

            border-radius: 50%;

            background: #2563eb;
            color: white;

            font-size: 36px;
            font-weight: bold;

            box-shadow: 0 8px 20px rgba(37, 99, 235, 0.35);
        }

        h1 {
            margin: 0 0 8px;

            color: #111827;

            font-size: 28px;
        }

        .subtitle {
            margin: 0 0 28px;

            color: #6b7280;

            font-size: 15px;
        }

        .info {
            text-align: left;

            margin-bottom: 24px;
        }

        .info-row {
            padding: 14px 0;

            border-bottom: 1px solid #e5e7eb;
        }

        .info-row:last-child {
            border-bottom: none;
        }

        .label {
            display: block;

            margin-bottom: 4px;

            color: #6b7280;

            font-size: 13px;
        }

        .value {
            color: #111827;

            font-size: 16px;
            font-weight: 600;

            word-break: break-word;
        }

        .status {
            display: inline-block;

            margin-top: 5px;

            padding: 6px 12px;

            border-radius: 999px;

            background: #dcfce7;
            color: #166534;

            font-size: 13px;
            font-weight: bold;
        }

        .logout {
            display: block;

            width: 100%;

            padding: 13px;

            border-radius: 8px;

            background: #ef4444;
            color: white;

            text-decoration: none;

            font-size: 16px;
            font-weight: bold;

            transition: 0.2s;
        }

        .logout:hover {
            background: #dc2626;

            transform: translateY(-1px);
        }

        @media (max-width: 480px) {

            .container {
                padding: 28px 22px;
            }

            h1 {
                font-size: 24px;
            }

        }

    </style>

</head>

<body>

<div class="container">

    <div class="avatar">
        <?= htmlspecialchars(strtoupper(substr($user["Nombre"], 0, 1))) ?>
    </div>

    <h1>
        Bienvenido, <?= htmlspecialchars($user["Nombre"]) ?>
    </h1>

    <p class="subtitle">
        Estos son los datos de tu cuenta
    </p>

    <div class="info">

        <div class="info-row">
            <span class="label">Usuario</span>
            <span class="value">
                <?= htmlspecialchars($user["Usuario"]) ?>
            </span>
        </div>

        <div class="info-row">
            <span class="label">Correo electrónico</span>
            <span class="value">
                <?= htmlspecialchars($user["Email"]) ?>
            </span>
        </div>

        <div class="info-row">
            <span class="label">ID de usuario</span>
            <span class="value">
                <?= htmlspecialchars($user["Id"]) ?>
            </span>
        </div>

        <div class="info-row">
            <span class="label">Estado</span>
            <span class="status">
                ● Cuenta activa
            </span>
        </div>

    </div>

    <a href="/logout" class="logout">
        Cerrar sesión
    </a>

</div>

</body>

</html>
```
