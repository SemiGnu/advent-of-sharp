f = open("16/16t1.txt", "r")
input = f.read()

bits = []
for c in input:
    bits += format(int(c, 16), 'b').zfill(4)

def parse(packet):
    version = int(''.join(packet[:3]),2) 
    type_id = int(''.join(packet[3:6]),2)
    content = None
    if type_id == 4:
        value = parse_literal(packet[6:])
        return int(''.join(value),2)
        

def parse_literal(packet):
    end = packet[0] == '0'
    if end:
        return packet[1:5]
    return packet[1:5] + parse_literal(packet[5:])

test = parse(bits)

print(''.join(bits))

print(test)