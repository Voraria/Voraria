#!/usr/bin/env node

const csv = require('fs').readFileSync('./Terraria.csv', 'utf8')
const lines = csv.split(/\r?\n/g)
const columns = lines.shift().split(',')

columns.shift()
columns.shift()

const tags = {}
for (const column of columns) {
    tags[column] = []
}
for (const line of lines) {
    const cells = line.split(',')
    for (let i = 0; i < columns.length; i++) {
        if (cells[i + 2]) tags[columns[i]].push({ name: cells[0], id: parseInt(cells[1]) })
    }
}

const code = `
using Terraria;
using Terraria.ID;
using static Terraria.ID.NPCID;

namespace VoreMod {
    public static class EntityTagLists
    {
${Object.keys(tags).map(tag => `
        public static bool Is${tag.replace(/\s+/g, '')}(int npcID)
        {
${tags[tag].length ? `
            switch(npcID)
            {
${tags[tag].map(pair => `                case ${pair.name && pair.id < 580 ? pair.name : pair.id}:`).join('\n')}
                    return true;
            }
` : ``}
            return false;
        }`).join('\n')}
    }
}
`

require('fs').writeFileSync('./EntityTagLists.cs', code, 'utf8')
