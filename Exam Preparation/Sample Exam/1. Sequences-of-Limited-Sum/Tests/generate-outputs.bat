FOR %%f in ("*.in.txt") DO (
	SETLOCAL EnableDelayedExpansion
    SET "file=%%f"
    _1.SequencesOfLimitedSum.exe < "%%f" > "!file:.in.txt=.out.txt!"
)
