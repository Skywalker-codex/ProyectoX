<?php

require_once "conexion.php";

if ($_SERVER["REQUEST_METHOD"] !== "POST") {
    header("Location: FRONTEND/signin.html");
    exit;
}

$nombre = trim($_POST["nombre"] ?? "");
$email = trim($_POST["email"] ?? "");
$password = $_POST["password"] ?? "";

if ($nombre === "" || $email === "" || $password === "") {
    die("Todos los campos son obligatorios.");
}

if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
    die("El correo electrónico no es válido.");
}

if (strlen($password) < 8) {
    die("La contraseña debe tener al menos 8 caracteres.");
}

$passwordHash = password_hash(
    $password,
    PASSWORD_DEFAULT
);

try {

    $sql = "INSERT INTO usuarios (nombre, email, password)
            VALUES (:nombre, :email, :password)";

    $stmt = $pdo->prepare($sql);

    $stmt->execute([
        ":nombre" => $nombre,
        ":email" => $email,
        ":password" => $passwordHash
    ]);

    header("Location: FRONTEND/login.html?registro=ok");
    exit;

} catch (PDOException $e) {

    if ($e->getCode() === "23000") {
        die("Este correo electrónico ya está registrado.");
    }

    die("No se pudo crear la cuenta.");
}