import os

SECRETS = os.environ.get("SECRETS")

file = open(".env", "a")
file.write(SECRETS)

file.close()
