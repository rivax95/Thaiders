
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


$userMail = $_POST["Email"];
//$EmailClean = filter_var($userMail,FILTER_SANITIZE_STRING,FILTER_FLAG_STRIP_LOW| FILTER_FLAG_STRIP_HIGH);
$password = $_POST["Password"];

//check 
$nameQuery = "SELECT Email,salt,hash,ban,FriendsList FROM ".$TableUser." WHERE Email= '". $userMail ."';";

$MailCheck = mysqli_query($conn,$nameQuery) or die("2: Conecxion no establecida"); // f
if(mysqli_num_rows($MailCheck) !=1){
	echo "5: Nombre de usuario no encontrado";
	exit();
}
//get loggin info

$existinginfo = mysqli_fetch_assoc($MailCheck);
$salt = $existinginfo["salt"];
$hash = $existinginfo["hash"];
$baned = $existinginfo["ban"];


$loginhash = crypt($password,$salt);
if($hash != $loginhash){
	echo "6: incorrect password";
	exit();
}
if($baned == 1){
	echo "7: Account Banned";
	exit();
}
echo "k" ;
echo ",".$existinginfo["FriendsList"];


?>