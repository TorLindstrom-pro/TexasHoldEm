# Notes

* ⚠️ **WIP**  
* ✅ **GREEN**  
* 🧠 **In Discovery**  
* ❌ **RED**  
* 📝 **TBD**  

### Goal: 
### Time 🍅
### Notes:

given a hand, and community cards, return the best possible outcome

Hand(new[] {"A♠", "A♦"}, new[] {"J♣", "5♥", "10♥", "2♥", "3♦"})
// ...should return ("pair", new[] {"A", "J", "10", "5"})
Hand(new[] {"A♠", "K♦"}, new[] {"J♥", "5♥", "10♥", "Q♥", "3♥"})
// ...should return ("flush", new[] {"Q", "J", "10", "5", "3"})