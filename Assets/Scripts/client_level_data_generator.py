import xml.etree.ElementTree as ET

def newClient(root, spawn_floor_number, target_floor_number, time_of_spawn):
    client = ET.SubElement(root, 'client')
    spawn_floor = ET.SubElement(client, 'spawnFloor')
    target_floor = ET.SubElement(client, 'targetFloor')
    time = ET.SubElement(client, 'timeOfSpawn')
    spawn_floor.text = str(spawn_floor_number)
    target_floor.text = str(target_floor_number)
    time.text = str(time_of_spawn)

filename = 'level1.xml'
root = ET.Element('level')

newClient(root, 1, 3, 0)
#newClient(root, 3, 2, 3)
#newClient(root, 1, 3, 8)

tree = ET.ElementTree(root)
tree.write(filename)

