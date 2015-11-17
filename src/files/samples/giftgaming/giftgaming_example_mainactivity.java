import com.soomla.Soomla;
import com.soomla.store.SoomlaStore;
import com.soomla.store.StoreInventory;
import com.soomla.store.exceptions.VirtualItemNotFoundException;
import com.giftgaming.giftgamingandroid.Giftgaming;

public class MainActivity extends ActionBarActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        // Initialize SOOMLA Store
        SoomlaStore.Initialize(new YourStoreAssetsImplementation());

        // Use regular Giftgaming() if you want to use sample API key
        Giftgaming gg = new Giftgaming("-- MY API KEY --");

        // Enable if you want to see example giftgaming Vault Button
        gg.autoDrawMode = false;

        // Pass in current activity and top level UI container
        gg.setMainActivity(this, R.id.mainContainer);

        // Pass in our own callbacks in just one function
        gg.setGiftCallbacks(new MyGameGifts());
    }
}
