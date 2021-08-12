<?php

namespace App\Http\Controllers\ApiControllers;

use DB;
use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use App\Models\User;

class UserController extends Controller
{
    public function __construct(){
        $this->middleware('auth:api',[]);
    }
    /**
     * Display a listing of the resource.
     *
     * @return \Illuminate\Http\Response
     */
    public function index()
    {
        // $data= DB::table('users')->get();
        // return $data;
        return User::all();
    }

    /**
     * Store a newly created resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @return \Illuminate\Http\Response
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function show(User $user)
    {
        // $data = DB::table("users")->where("use_id","=",$use_id)->first();
        // return $data;
        return $user;
    }

    /**
     * Update the specified resource in storage.
     *
     * @param  \Illuminate\Http\Request  $request
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function update(Request $request, $use_id)
    {
        $use_name = $request->use_name;//<=>$name = $_POST["name"]
    	$password = $request->password;
        $use_phone = $request->use_phone;
    	//update ban ghi
    	//update(array("tencot1"=>giatri,"tencot2"=>giatri)) -> update ban ghi tuong ung
    	DB::table("users")->where("use_id","=",$use_id)->update(array("use_name"=>$use_name, "use_phone"=>$use_phone));
    	//neu password khong rong thi update password
    	if($password != ""){
    		//ma hoa password
    		$password = bcrypt($password);
    		//update ban ghi
    		DB::table("users")->where("use_id","=",$use_id)->update(array("password"=>$password));
    	}
        return response()->json(['message'=> 'Sửa thông tin người dùng thành công']);
    }

    /**
     * Remove the specified resource from storage.
     *
     * @param  int  $id
     * @return \Illuminate\Http\Response
     */
    public function destroy($id)
    {
        //
    }
}
