# Game format specifications

This section list the game assets and their format. You can find more
information about the specification in each sub-page.

## Containers

Most of the assets are packed with the [`ALAR` container format](./alar.md).

Some individual files are furthermore compressed with the `DSCP` format.

## Texts

The game doesn't have a standard format to store text. It's usually included in
data structures, stored in files with `.bin` extension. Typically, we have
pointers and sentences, but each file is different, that's why we have a format
for each type of file. The [text specification](./texts.md) have more details.

- `battle/tutorial*.bin`: battle tutorial text
- `bin/ability_t.bin`: abilities
- `bin/bgm.bin`: battle background music
- `bin/chr_b_t.bin`: battle character abilities
- `bin/chr_s_t.bin`: support character abilities
- `bin/clearlst.bin`: stage goals
- `bin/commwin.bin`: common window messages
- `bin/demo.bin`: world names in demo player menu
- `bin/InfoDeck.aar/*`: koma explanation in gallery menu
- `bin/infoname.bin`: main menu helper names
- `bin/komatxt.bin`: koma names
- `bin/location.bin`: player location
- `bin/piece.bin`: manga author and info
- `bin/pname.bin`: player name titles
- `bin/rulemess.bin` stage rules
- `bin/stage.bin` stage names
- `bin/title.bin` manga names
- `deck/Deck.aar/*/*`: deck texts
- `deckmake/tutorial.bin`: deck tutorials
- `jgalaxy/jgalaxy.aar/*`: JGalaxy texts
- `jquiz/jquiz.aar/`: quiz questions

## Fonts

Pending documentation.

## Graphics

The main formats for images are:

- [`DSTX`](./dtx.md)
- `DSIG`

Additionally, _komas_ have their own format documented in the
[koma specification](./koma.md).

## Sound

- `sound/JS2_sound.sdat`: standard Nitro `SDAT` container and sound formats.

## Data structures

These files have different data structures to support the game features.

- `bin/ability.bin`
- `bin/chr_b.bin`
- `bin/chr_s.bin`
- `bin/clear.bin`
- `bin/exadd.bin`
- `bin/jpower.bin`
- `bin/secret.bin`
- `bin/state.bin`
- `ChildRom/`: download-play transferable ROM and icon.
- `dwc/utility.bin`: assets for the online communication.
- `opening/PassMark.bin`
- `opening/pattern.bin`

## General info

### Absolute pointers

Absolute pointers mean that the text is in the pointer offset plus the position
of the pointer. If the position of the pointer is 0x04 and the value is 0x100,
the text will be in 0x104.
