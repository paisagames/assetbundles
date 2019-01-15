<?php

$host_name  =   "sql9.freemysqlhosting.net";
$database   =   "sql9270836";
$user_name  =   "sql9270836";
$password   =   "xxxxxxx";


$conn   =   new mysqli($host_name,$user_name,$password,$database);
$conn->set_charset('utf8');
if($conn->connect_error){
    die("Connection failed:" . $conn->connect_error);
}

$sql ="INSERT INTO usuarios_restaurantes (usuario,restaurant,correo,password,modalidad,fecha_de_registro,registrado_por) VALUES ('php','php2','php3','prueba_android','php','prueba_android','prueba_android');";
$result =   $conn->query($sql);

$conn->close();
?>
