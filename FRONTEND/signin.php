<?php

require_once __DIR__ . "/db.php";

if ($_SERVER["REQUEST_METHOD"] !== "POST") {
    header("Location: /signin");
    exit;
}

$nombre = trim($_POST["nombre"] ?? "");
$email = trim($_POST["email"] ?? "");
$password = $_POST["password"] ?? "";

if ($nombre === "" || $email === "" || $password === "") {
    exit("Todos los campos son obligatorios.");
}

if (!filter_var($email, FILTER_VALIDATE_EMAIL)) {
    exit("El correo electrónico no es válido.");
}

if (strlen($password) < 8) {
    exit("La contraseña debe tener al menos 8 caracteres.");
}

$passwordHash = password_hash($password, PASSWORD_DEFAULT);

try {

    $sql = "
        INSERT INTO usuarios
            (Nombre, Email, Password, IsActive)
        VALUES
            (:nombre, :email, :password, 1)
    ";

    $stmt = $pdo->prepare($sql);

    $stmt->execute([
        ":nombre" => $nombre,
        ":email" => $email,
        ":password" => $passwordHash
    ]);

    header("Location: /login?registro=ok");
    exit;

} catch (PDOException $e) {

    error_log("ERROR REGISTRO: " . $e->getMessage());

    http_response_code(500);

    echo "<h2>Error al registrar usuario</h2>";
    echo "<pre>";
    echo htmlspecialchars($e->getMessage());
    echo "</pre>";

    exit;
}