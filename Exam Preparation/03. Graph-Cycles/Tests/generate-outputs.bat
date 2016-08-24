FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Graph-Cycles.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
