Import-Module '.\packages\psake.4.3.1.0\tools\psake.psm1';

Invoke-psake -buildFile default.ps1 -framework '4.0';

$buildStatusCode = $LASTEXITCODE

remove-module [p]sake -erroraction silentlycontinue;

exit $buildStatusCode
