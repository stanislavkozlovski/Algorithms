FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Bridges.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
