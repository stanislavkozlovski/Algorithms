FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    Group-Permutations.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
