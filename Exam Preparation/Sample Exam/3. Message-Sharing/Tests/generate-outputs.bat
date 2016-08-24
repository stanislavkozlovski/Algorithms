FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Message-Sharing.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
