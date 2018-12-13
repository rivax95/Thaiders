<?php
	$servername = "localhost";
	$username =  "root";
	$password = "mercadat";
	$dbName = "thaidersusers";
	$Table ="";
	$columnname="";
	$UserSearch="";
	//Make Connection
	$conn = new mysqli($servername, $username, $password, $dbName);
	//Check Connection
	if(!$conn){
		die("Connection Failed. ". mysqli_connect_error());
	}
	$sql2 ="SELECT * FROM ".$Table." WHERE ".$columnname." = '["$UserSearch"]'";
	$sql = "SELECT id, Nombre, Apellidos, Nacionalidad,Nick,Password,ban FROM users";
	$result = mysqli_query($conn ,$sql2,$Table,$columnname,$UserSearch);
	
	
	if(mysqli_num_rows($result) > 0){
		//show data for each row
		while($row = mysqli_fetch_assoc($result)){
			echo "".$row['id'] . " ;".$row['Nombre']. ";".$row['Apellidos']. ";".$row['Nacionalidad'] . ";".$row['Nick'] . ";".$row['Password'] .
			 "; ".$row['ban'] ;
		}
	}
	
	
	
	


?>