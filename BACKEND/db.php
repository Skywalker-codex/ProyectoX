<?php

/*$server = "127.0.0.1";*/
$server = "79.112.109.165";
$database = "Produccion";
$username = "Isaac.esteve";
$password = "Isaac.esteve";

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

