echo init > dest.txt

call PurpleGun.Filler a.txt
call PurpleGun.Filler b.txt
call PurpleGun.Filler c.txt

start PurpleGun.Launcher dest.txt custom
timeout 2 > nul

start PurpleGun.Reader custom a.txt
start PurpleGun.Reader custom b.txt
start PurpleGun.Reader custom c.txt
