FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Sticks.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
