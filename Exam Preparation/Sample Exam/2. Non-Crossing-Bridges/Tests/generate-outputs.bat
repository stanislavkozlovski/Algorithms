FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    _2.NonCrossingBridges.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
