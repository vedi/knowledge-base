import com.soomla.store.StoreInventory;
import com.giftgaming.giftgamingandroid.GiftgamingGifts;
import com.giftgaming.giftgamingandroid.Giftgaming;

public class MyGameGifts implements GiftgamingGifts {

    public MyGameGifts() {
    }

    ...

    // The giftCode is set from giftgaming Dashboard
    // Must correspond to your Soomla Store itemId
    public void giftClosed(string giftCode) {
        try {
            int AMOUNT = 1; // Amount of thing you want to gift
            StoreInventory.GiveVirtualItem(giftCode, AMOUNT);
        } catch(VirtualItemNotFoundException e) {
            // Currency not identified
        }
    }
}
