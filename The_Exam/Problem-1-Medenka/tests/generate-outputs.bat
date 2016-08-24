FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Medenka.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
