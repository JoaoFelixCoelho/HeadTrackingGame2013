<?php 
	$db = mysql_connect('localhost', 'root', 'root') or die('No se pudo conectar: ' . mysql_error()); 
	mysql_select_db('score') or die('No se pudo seleccionar la base de datos');

	$name = mysql_real_escape_string($_GET['name'], $db); 
	$score = mysql_real_escape_string($_GET['score'], $db); 
	$hash = $_GET['hash']; 

	$secretKey="martha";

	$real_hash = md5($name . $score . $secretKey); 
	if($real_hash == $hash) {
		$query = "insert into scores values (NULL, '$name', '$score');"; 
		$result = mysql_query($query) or die('Query failed: ' . mysql_error()); 
	} 
?>