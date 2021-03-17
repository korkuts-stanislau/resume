import random


types = ["int", "string", "double"]
chars = "abcdefghijklmnopqrstuvwxyz"


def get_random_string(str_size: int) -> str:
    gen_string: str = ""
    for i in range(random.randint(4, str_size)):
        gen_string += chars[random.randint(0, len(chars) - 1)]
    return gen_string


count: int = 1000


with open("bigdata.txt", "wt") as wf:
    for i in range(count):
        line = f"{get_random_string(10)};{types[random.randint(0, len(types) - 1)]};{random.randint(0, 5000)}\n"
        wf.write(line)
