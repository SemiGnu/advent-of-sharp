import math

f = open("16/16.txt", "r")
input = f.read()

# input = '8A004A801A8002F478'

bits = []
for c in input:
    bits += format(int(c, 16), 'b').zfill(4)

class Packet:
    def __init__(self, version, type_id, content, content_length):
        self.version = version
        self.type_id = type_id
        self.content = content
        self.content_length = content_length

    def __str__(self):
        return self.str(0)

    def str(self, ind):
        return f'{"-" : >{4*ind}} v{self.version}/type id: {self.type_id}\n' + (f'{" " : >{4*ind}} {self.content}\n' if self.type_id == 4 else ''.join(list(map(lambda x, ind = ind: x.str(ind + 1), self.content))))

    def eval_version(self):
        if self.type_id == 4:
            return self.version
        return self.version + sum(list(map(lambda x: x.eval_version(), self.content)))

    def eval(self):
        match self.type_id:
            case 0:
                return sum(list(map(lambda x: x.eval(), self.content)))
            case 1:
                return math.prod(list(map(lambda x: x.eval(), self.content)))
            case 2:
                return min(list(map(lambda x: x.eval(), self.content)))
            case 3:
                return max(list(map(lambda x: x.eval(), self.content)))
            case 4:
                return self.content
            case 5:
                return int(self.content[0].eval() > self.content[1].eval())
            case 6:
                return int(self.content[0].eval() < self.content[1].eval())
            case 7:
                return int(self.content[0].eval() == self.content[1].eval())

def parse(packet):
    version = int(''.join(packet[:3]),2) 
    type_id = int(''.join(packet[3:6]),2)
    if type_id == 4:
        result = parse_literal(packet[6:])
        value = int(''.join(result),2)
        content_length = int(6 + len(result) * 1.25)
        remaining = packet[content_length:]
        return (Packet(version, type_id, value, content_length),remaining)
    
    length_type_id = int(packet[6])
    length_length =  15 if length_type_id == 0 else 11
    idx = 7+length_length
    length = int(''.join(packet[7:idx]),2) 
    sub_packets = []
    sub_packet_length = 0
    remaining = packet[idx:]
    while (length_type_id == 0 and sub_packet_length < length) or (length_type_id == 1 and len(sub_packets) < length):
        (pck, remaining) = parse(remaining)
        idx += pck.content_length
        sub_packet_length += pck.content_length
        sub_packets.append(pck)
    return (Packet(version, type_id, sub_packets, idx), remaining)

def parse_literal(packet):
    end = packet[0] == '0'
    if end:
        return packet[1:5]
    return packet[1:5] + parse_literal(packet[5:])

(result, remaining) = parse(bits)

print(''.join(bits))

print(result)
print(result.eval_version())
print(result.eval())