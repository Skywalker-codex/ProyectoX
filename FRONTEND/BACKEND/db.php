<?php

$server = "127.0.0.1";
$database = "Produccion";
$username = "isaac.esteve";
$password = "isaac.esteve";

$dsn = "sqlsrv:Server=$server;Database=$database;Encrypt=yes;TrustServerCertificate=yes";

try {

    $pdo = new PDO($dsn, $username, $password);

    $pdo->setAttribute(
        PDO::ATTR_ERRMODE,
        PDO::ERRMODE_EXCEPTION
    );

} catch (PDOException $e) {

    error_log($e->getMessage());

    http_response_code(500);

    exit("Error interno del servidor");
}

