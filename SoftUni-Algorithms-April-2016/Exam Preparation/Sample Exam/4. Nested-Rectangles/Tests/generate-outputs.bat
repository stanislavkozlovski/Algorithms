FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Nested-Rectangles.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
