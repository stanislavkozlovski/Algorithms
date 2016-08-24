FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Robery.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
