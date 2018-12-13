<?php
	$servername = 'localhost';
	$username =  'root';
	$password = 'mercadat';
	$dbName = 'thaidersusers';
	
	$TableUser= "users";
$conn =new mysqli($servername, $username, $password, $dbName);

if(mysqli_connect_errno()){
	echo"1: Conecxion fallida"; //falla la conecxion
	exit();
}
$Nombre = $_POST["Nombre"];
$apellidos = $_POST["Apellidos"];
$Nacimiento = $_POST["Nacimiento"];
$Nacionalidad= $_POST["Nacionalidad"];
$Nick = $_POST["Nick"];
$Ban = $_POST["Ban"];
$userMail = $_POST["Email"];
$password = $_POST["Password"];

//check 
$nameQuery = "SELECT Email FROM ".$TableUser." WHERE Email='". $userMail ."';";

$MailCheck = mysqli_query($conn,$nameQuery) or die("2: Conecxion no establecida"); // f
if(mysqli_num_rows($MailCheck) >0){
	echo "3: Email ya existe";
	exit();
}
// Añadimos usuarios

$salt = "\$5\$rounds=5000\$"."steamehams" . $userMail . "\$";
$hash = crypt($password,$salt);
$insertuserquery = "INSERT INTO ".$TableUser."(Nombre,Apellidos,Email,Nacimiento,Nacionalidad,Nick,Password,Ban,hash,salt) VALUES ('".$Nombre."','".$apellidos."','".$userMail."','".$Nacimiento."','".$Nacionalidad."','".$Nick."','".$password."','".$Ban. "','".$hash. "','".$salt. "');";
mysqli_query($conn,$insertuserquery) or die("4: Fallo en la consulta del registro"); // error code 4

 echo ("0");
?>