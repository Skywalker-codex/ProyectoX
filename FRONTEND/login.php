<?php

session_start();

require_once __DIR__ . "../BACKEND/db.php";

$email = $_POST["email"] ?? "";
$password = $_POST["password"] ?? "";

$sql = "
    SELECT Id, Email, Password
    FROM usuarios
    WHERE Email = :email
      AND IsActive = 1
";

$stmt = $pdo->prepare($sql);

$stmt->execute([
    ":email" => $email
]);

$user = $stmt->fetch(PDO::FETCH_ASSOC);

if (!$user || !password_verify($password, $user["Password"])) {

    exit("Usuario o contraseña incorrectos");
}

session_regenerate_id(true);

$_SESSION["user_id"] = $user["Id"];
$_SESSION["email"] = $user["Email"];

header("Location: /portal.html");
exit;