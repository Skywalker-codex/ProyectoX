<?php

session_start();

if (!isset($_SESSION["user_id"])) {
    header("Location: /login");
    exit;
}

require_once "/var/www/ProyectoX/BACKEND/db.php";

$userId = $_SESSION["user_id"];

$sql = "
    SELECT Id, Email, Nombre, Usuario, IsActive
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