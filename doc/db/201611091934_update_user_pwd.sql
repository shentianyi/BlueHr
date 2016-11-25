use BlueCarGps
go

update [User] set pwdSalt='ZVqcOaidXIYon8nzi+C1Gqf6kHlgrZFWjoAHBX5zo9/JyDfC',
                  pwd='5086e4b986ee1ebd21694ce7d32a5269'
                  where pwdSalt is null;