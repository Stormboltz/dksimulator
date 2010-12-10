<?php

$realmName = $_GET['r'];
$characterName = $_GET['n'];
$region = $_GET['region'];
$espace= " ";
$plus = "+";
$minus = "-";
$realm = str_replace($espace,$minus,$realmName);
$parame = "?r=".$realm."&n=".$characterName;

$uriString = "http://".$region.".battle.net/wow/en/character/".$realm."/".$characterName."/advanced";

echo $uriString;
echo get_xml_data_from_url($uriString,"en");

function get_xml_data_from_url($url,$lang)
  {        
    $url_array = parse_url($url);
    $fp = fsockopen($url_array['host'], 80, $errno, $errstr, 5); 
    $send = "GET " . $url_array[path] . "?" . $url_array[query] ." HTTP/1.0\r\n";
    $send .= "Host: " . $url_array[host] . " \r\n";
    //$send .= "User-Agent: Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)\r\n";
	$send .= "User-Agent: Mozilla/5.0 (X11; U; Linux i686; pl-PL; rv:1.9.0.2) Gecko/20121223 Ubuntu/9.25 (jaunty) Firefox/3.8\r\n";
    $send .= "Accept-Language: " . $lang . "\r\n";
    $send .= "Connection: Close\r\n\r\n";
	//echo $fp;
	//echo $send;
    fwrite($fp, $send);
    while ($fp && !feof($fp))
    {
      $headerbuffer = fgets($fp, 1024);
      if (urlencode($headerbuffer) == "%0D%0A")
      {
        break;
      }
    }
    $xml_data = '';
    while (!feof($fp))
    {
      $xml_data .= fgets($fp, 1024);
    }
    fclose($fp);
    return $xml_data;
  }  
?>