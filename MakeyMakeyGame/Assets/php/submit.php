<?php 
    $DB_SERVER = "localhost";
    $DB_USER = "teunkhm32_makey";
    $DB_PASS = "teunkhm32_makey";
    $DB_NAME = "superpassword";

    $USER_NAME = $_GET['username'];
    $USER_SCORE = $_GET['score'];
    $USER_DATE = date("d/m/Y");
    
    $con = mysqli_connect($DB_SERBER, $DB_USER, $DB_PASS, $DB_NAME);

    $data = mysqli_query($con, "INSERT INTO highscore(USERNAME, SCORE, DATE)
        VALUES('$USER_NAME', '$USER_SCORE', '$USER_DATE')");

    mysqli_close($con);
?>