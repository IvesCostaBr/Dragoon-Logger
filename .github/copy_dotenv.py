import os

SECRETS = os.environ.get("SECRETS")

file = open("./Dragoon-Log/.env", "a")
file.write(SECRETS)

file.close()
