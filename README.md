# MoniChromaticTranscendencePathCalculator

Calculates the shortest or lowest-cost crafting path for the "Chromatic Transcendence" recipe in the Minecraft modpack [Monifactory](https://github.com/ThePansmith/Monifactory).

Currently, it only supports calculating crafting paths for a single machine. If you are using multiple machines, you may find a better crafting path, but this is not currently supported and may be added in the future.

## Chroma

This machine has 12 chromas. The recipe requires a specific chroma and will change the current chroma.

| Name    | Type      |
| ------- | --------- |
| Red     | Primary   |
| Orange  | Other     |
| Yellow  | Secondary |
| Lime    | Other     |
| Green   | Primary   |
| Teal    | Other     |
| Cyan    | Secondary |
| Azure   | Other     |
| Blue    | Primary   |
| Indigo  | Other     |
| Magenta | Secondary |
| Pink    | Other     |

```
         _- Red  -_
    Pink            Orange
    /                 \
Magenta               Yellow
  |                     |
Indigo                Lime
  |                     |
Blue                  Green
    \                 /
    Azure           Teal
         '- Cyan -'
```

## Chromatic Stabilizer

If the current chroma is a primary chroma, subtract 2 units. If the current chroma is a secondary chroma, add 2 units. Otherwise, change the current chroma to the nearest primary chroma.

## Chromatic Capacitor

Stores the primary and secondary chroma values. If the current chroma differs from the chroma value in the provided capacitor by 2 units, shift the current chroma 1 unit towards the chroma value in the capacitor.

## Prismatic Core

Item used in Chromatic Transcendence. Each recipe produces the next Prismatic Core in sequence. Required products are Active Prismatic Cores and Supercritical Prismatic Cores.

| Order | Name          | Required Chroma | Next Chroma |
| ----- | ------------- | --------------- | ----------- |
| 1     | Inert         | Red             | Green       |
| 2     | Red           | Yellow          | Red         |
| 3     | Yellow        | Green           | Blue        |
| 4     | Green         | Cyan            | Green       |
| 5     | Cyan          | Blue            | Red         |
| 6     | Blue          | Magenta         | Blue        |
| 7     | Active        | Orange          | Green       |
| 8     | Orange        | Lime            | Indigo      |
| 9     | Lime          | Teal            | Blue        |
| 10    | Teal          | Azure           | Orange      |
| 11    | Azure         | Indigo          | Red         |
| 12    | Indigo        | Pink            | Teal        |
| 13    | Supercritical | -               | -           |
