VerifyFIPSMode
==============

A simple tool to verify whether your Windows Server 2008 R2 machine is running in FIPS mode.

Double click it, or run from the command line.

If the program spits out a bunch of hashes to your command window, then your machine is not running in FIPS mode - or Microsoft has made changes to its FIPS certification and all the algorithms I have listed as not supported in FIPS mode are now suddenly working.

I created this tool to try and verify some potential instability with Microsoft's FIPS Enabled registry setting:
http://support.microsoft.com/kb/811833

Windows 2008 and Vista:
HKLM\System\CurrentControlSet\Control\Lsa\FIPSAlgorithmPolicy\Enabled

Windows 2003 and XP:
HKLM\System\CurrentControlSet\Control\Lsa\FIPSAlgorithmPolicy