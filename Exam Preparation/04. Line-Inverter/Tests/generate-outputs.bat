FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Line-Inverter.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
