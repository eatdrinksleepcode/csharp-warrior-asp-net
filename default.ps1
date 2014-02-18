properties {
  $buildMessage = 'Building Project!'
}

Task Default -depends MsBuild;

Task MsBuild {
	$buildMessage
	try {
		Exec { msbuild "csharpwarrior.sln" /p:VisualStudioVersion=12.0 }
	} catch {
		Exit 11
	}
}
